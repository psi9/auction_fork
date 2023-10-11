import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";

import { Lot } from "../objects/Entities";
import { State } from "../objects/Enums";

import LotHttpRepository from "../repositories/implementations/LotHttpRepository";

interface ILotContext {
  lots: Lot[];

  getLotsByAuction: () => Promise<Lot[]>;
  setAuctionId: (auctionId: string) => void;
}

export const LotContext = createContext<ILotContext | undefined>(undefined);

export const LotProvider = ({ children }: { children: ReactNode }) => {
  const initialLots: Lot[] = [
    {
      id: "564BBF8E-3B61-4EAD-AF62-05F72A4654E5",
      name: "",
      description: "",
      auctionId: "564BBF8E-3B61-4EAD-AF62-05F72A4654E5",
      startPrice: 0,
      buyoutPrice: 0,
      betStep: 0,
      state: State.awaiting,
      bets: [],
      images: [],
    },
  ];

  const lotRepository = new LotHttpRepository("https://localhost:7132/");

  const [lots, setLots] = useState<Lot[]>(initialLots);
  const [curAuctionId, setCurAuctionId] = useState<string>("");

  useEffect(() => {
    async function fetchLots() {
      setLots(await lotRepository.getAsync());
    }

    fetchLots();
  });

  async function getLotsByAuction(): Promise<Lot[]> {
    return await lotRepository.getByAuctionAsync(curAuctionId);
  }

  function setAuctionId(auctionId: string) {
    setCurAuctionId(auctionId);
  }

  return (
    <LotContext.Provider value={{ lots, getLotsByAuction, setAuctionId }}>
      {children}
    </LotContext.Provider>
  );
};

export const useLotContext = () => useContext(LotContext);
