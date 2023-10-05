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

export const LotContext = createContext<Lot[]>([]);

export const LotProvider = ({ children }: { children: ReactNode }) => {
  const initialLots: Lot[] = [
    {
      id: "",
      name: "",
      description: "",
      auctionId: "",
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

  useEffect(() => {
    async function fetchLots() {
      setLots(await lotRepository.getAsync());
    }

    fetchLots();
  });

  return <LotContext.Provider value={lots}>{children}</LotContext.Provider>;
};

export const useLotContext = () => useContext(LotContext);
