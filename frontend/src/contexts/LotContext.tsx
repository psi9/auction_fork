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
import { AuctionContext } from "./AuctionContext";

interface ILotContext {
  lots: Lot[] | undefined;

  getLotsByAuction: () => Promise<Lot[] | undefined>;
  createLot: (formData: FormData) => void;
}

export const LotContext = createContext<ILotContext>({} as ILotContext);

const lotRepository = new LotHttpRepository("https://localhost:7132/");

export const LotProvider: React.FC<PropsWithChildren> = ({ children }) => {
  const [lots, setLots] = useState<Lot[] | undefined>([]);

  const { user } = useContext(UserAuthorizationContext);
  const { curAuctionId } = useContext(AuctionContext);

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

  return (
    <LotContext.Provider value={{ lots, getLotsByAuction, createLot }}>
      {children}
    </LotContext.Provider>
  );
};
