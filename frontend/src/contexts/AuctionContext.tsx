import { createContext, useContext } from "react";

import { Auction } from "../domain/Entities";

export const AuctionContext = createContext<Auction[]>([]);

export const useAuctionContext = () => useContext(AuctionContext);
