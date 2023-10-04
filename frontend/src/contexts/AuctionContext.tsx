import { createContext, useContext } from "react";

import { Auction } from "../domain/Entities";

import { randomUUID } from "crypto";
import { State } from "../domain/Enums";

export const AuctionContext = createContext<Auction[]>([
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
]);

export const useAuction = () => useContext(AuctionContext);
