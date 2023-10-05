import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";

import { Auction } from "../objects/Entities";
import { State } from "../objects/Enums";

import AuctionHttpRepository from "../repositories/implementations/AuctionHttpRepository";

export const AuctionContext = createContext<Auction[]>([]);

export const AuctionProvider = ({ children }: { children: ReactNode }) => {
  const initialAuctions: Auction[] = [
    {
      id: "",
      name: "",
      description: "",
      dateStart: new Date(),
      dateEnd: new Date(),
      authorId: "",
      state: State.awaiting,
      lots: [],
    },
  ];

  const auctionRepository = new AuctionHttpRepository(
    "https://localhost:7132/"
  );

  const [auctions, setAuctions] = useState<Auction[]>(initialAuctions);

  useEffect(() => {
    async function fetchAuctions() {
      setAuctions(await auctionRepository.getAsync());
    }

    fetchAuctions();
  });

  return (
    <AuctionContext.Provider value={auctions}>
      {children}
    </AuctionContext.Provider>
  );
};

export const useAuctionContext = () => useContext(AuctionContext);
