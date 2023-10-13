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

interface IAuctionContext {
  auctions: Auction[];

  createAuction: (
    title: string,
    description: string,
    authorId: string
  ) => Promise<void>;
}

export const AuctionContext = createContext<IAuctionContext | undefined>(
  undefined
);

const auctionRepository = new AuctionHttpRepository("backend");

export const AuctionProvider = ({ children }: { children: ReactNode }) => {
  const [auctions, setAuctions] = useState<Auction[]>([]);

  useEffect(() => {
    async function fetchAuctions() {
      setAuctions(await auctionRepository.getAsync());
    }

    fetchAuctions();
  }, []);

  async function createAuction(
    title: string,
    description: string,
    authorId: string
  ) {
    const auction: Auction = {
      id: "B5FEA3BD-F650-4D61-BE5F-0A1411809E4F",
      name: title,
      description: description,
      dateStart: new Date(),
      dateEnd: new Date(),
      authorId: authorId,
      state: State.awaiting,
      lots: [],
    };

    await auctionRepository.postAsync(auction);
  }

  return (
    <AuctionContext.Provider value={{ auctions, createAuction }}>
      {children}
    </AuctionContext.Provider>
  );
};

export const useAuctionContext = () => useContext(AuctionContext);
