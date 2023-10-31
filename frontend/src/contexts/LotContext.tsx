import {
  PropsWithChildren,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";

import { Lot } from "../objects/Entities";

import LotHttpRepository from "../repositories/implementations/LotHttpRepository";
import { UserAuthorizationContext } from "./UserAuthorizationContext";

interface ILotContext {
  lots: Lot[] | undefined;

  curAuctionId: string;

  getLotsByAuction: () => Promise<Lot[] | undefined>;
  setAuctionId: (auctionId: string) => void;

  createLot: (formData: FormData) => void;
}

export const LotContext = createContext<ILotContext>({} as ILotContext);

const lotRepository = new LotHttpRepository("https://localhost:7132/");

export const LotProvider: React.FC<PropsWithChildren> = ({ children }) => {
  const [lots, setLots] = useState<Lot[] | undefined>([]);
  const [curAuctionId, setCurAuctionId] = useState<string>("");
  const { user } = useContext(UserAuthorizationContext);

  useEffect(() => {
    const fetchLots = async () => {
      if (!user) return;
      setLots(await lotRepository.getAsync());
    };

    fetchLots();
  }, [user]);

  const getLotsByAuction = async (): Promise<Lot[] | undefined> => {
    if (!curAuctionId) return;
    return await lotRepository.getByAuctionAsync(curAuctionId);
  };

  const createLot = async (formData: FormData) => {
    await lotRepository.createLotAsync(formData);
  };

  const setAuctionId = (auctionId: string) => {
    setCurAuctionId(auctionId);
  };

  return (
    <LotContext.Provider
      value={{ lots, curAuctionId, getLotsByAuction, setAuctionId, createLot }}
    >
      {children}
    </LotContext.Provider>
  );
};
