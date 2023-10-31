import {
  PropsWithChildren,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";

import { Auction } from "../objects/Entities";
import { State } from "../objects/Enums";

import AuctionHttpRepository from "../repositories/implementations/AuctionHttpRepository";
import { UserAuthorizationContext } from "./UserAuthorizationContext";

interface IAuctionContext {
  auctions: Auction[] | undefined;

  createAuction: (
    title: string,
    description: string,
    authorId: string
  ) => Promise<void>;
  getAuction: (id: string) => Promise<Auction | undefined>;
  deleteAuction: (auctionId: string) => Promise<void>;
}

export const AuctionContext = createContext<IAuctionContext>(
  {} as IAuctionContext
);

const auctionRepository = new AuctionHttpRepository("https://localhost:7132/");

export const AuctionProvider: React.FC<PropsWithChildren> = ({ children }) => {
  const [auctions, setAuctions] = useState<Auction[] | undefined>([]);
  const { user } = useContext(UserAuthorizationContext);

  useEffect(() => {
    const fetchAuctions = async () => {
      if (!user) return;

      setAuctions(await auctionRepository.getAsync());
    };

    fetchAuctions();
  }, [user]);

  const createAuction = async (
    title: string,
    description: string,
    authorId: string
  ) => {
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
  };

  const getAuction = async (id: string): Promise<Auction | undefined> => {
    const auction = await auctionRepository.getByIdAsync(id);
    if (!auction) return;
    return auction;
  };

  const deleteAuction = async (auctionId: string) => {
    await auctionRepository.deleteAsync(auctionId);
  };

  return (
    <AuctionContext.Provider
      value={{ auctions, createAuction, getAuction, deleteAuction }}
    >
      {children}
    </AuctionContext.Provider>
  );
};
