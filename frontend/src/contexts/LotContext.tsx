import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";

import { Lot } from "../objects/Entities";

import LotHttpRepository from "../repositories/implementations/LotHttpRepository";

interface ILotContext {
  lots: Lot[];

  getLotsByAuction: () => Promise<Lot[]>;
  setAuctionId: (auctionId: string) => void;

  createLot: (lot: Lot) => void;
}

export const LotContext = createContext<ILotContext | undefined>(undefined);

const lotRepository = new LotHttpRepository("http://localhost:7132/");

export const LotProvider = ({ children }: { children: ReactNode }) => {
  const [lots, setLots] = useState<Lot[]>([]);
  const [curAuctionId, setCurAuctionId] = useState<string>("");

  useEffect(() => {
    async function fetchLots() {
      setLots(await lotRepository.getAsync());
    }

    fetchLots();
  }, []);

  async function getLotsByAuction(): Promise<Lot[]> {
    if (!curAuctionId) return [];
    return await lotRepository.getByAuctionAsync(curAuctionId);
  }

  async function createLot(lot: Lot) {
    await lotRepository.postAsync(lot);
  }

  function setAuctionId(auctionId: string) {
    setCurAuctionId(auctionId);
  }

  return (
    <LotContext.Provider
      value={{ lots, getLotsByAuction, setAuctionId, createLot }}
    >
      {children}
    </LotContext.Provider>
  );
};

export const useLotContext = () => useContext(LotContext);
