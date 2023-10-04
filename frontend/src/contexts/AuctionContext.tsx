import { ReactNode, createContext, useContext, useState } from "react";
import { Auction } from "../domain/Entities";
import { State } from "../domain/Enums";
import { randomUUID } from "crypto";

export const AuctionContext = createContext<Auction[]>([]);

export const AuctionProvider = ({ children }: { children: ReactNode }) => {
  const initialAuctions: Auction[] = [
    {
      id: randomUUID(),
      name: "",
      description: "",
      dateStart: new Date(),
      dateEnd: new Date(),
      authorId: randomUUID(),
      state: State.awaiting,
      lots: [],
    },
  ];

  const [auctions, setAuctions] = useState<Auction[]>(initialAuctions);

  return (
    <AuctionContext.Provider value={auctions}>
      {children}
    </AuctionContext.Provider>
  );
};

export const useAuctionContext = () => useContext(AuctionContext);
