import {
  PropsWithChildren,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";

import { Lot } from "../objects/Entities";

import LotHttpRepository from "../repositories/implementations/LotHttpRepository";
import { UserAuthorityContext } from "./UserAuthorityContext";

interface ILotContext {
  lots: Lot[] | undefined;

  getLotsByAuction: () => Promise<Lot[] | undefined>;
  setAuctionId: (auctionId: string) => void;

  createLot: (lot: Lot) => void;
}

export const LotContext = createContext<ILotContext>({} as ILotContext);

const lotRepository = new LotHttpRepository(
  "https://adm-webbase-66.partner.ru:7132/"
);

export const LotProvider: React.FC<PropsWithChildren> = ({ children }) => {
  const [lots, setLots] = useState<Lot[] | undefined>([]);
  const [curAuctionId, setCurAuctionId] = useState<string>("");
  const { user } = useContext(UserAuthorityContext);

  useEffect(() => {
    async function fetchLots() {
      if (!user) return;

      setLots((await lotRepository.getAsync()).data);
    }

    fetchLots();
  }, [user]);

  async function getLotsByAuction(): Promise<Lot[] | undefined> {
    if (!curAuctionId) return [];
    return (await lotRepository.getByAuctionAsync(curAuctionId)).data;
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
