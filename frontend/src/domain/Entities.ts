import { UUID } from "crypto";

import { State } from "./Enums"

export type User = {
  id: UUID;
  name: string;
  email: string;
  password: string;
  token: string;
};

export type Bet = {
  id: UUID;
  value: number;
  lotId: UUID;
  userId: UUID;
  dateTime: Date;
};

export type Image = {
  id: UUID;
  lotId: UUID;
  path: string;
};

export type Lot = {
  id: UUID;
  name: string;
  description: string;
  auctionId: UUID;
  startPrice: number;
  buyoutPrice: number;
  betStep: number;
  state: State;
  bets: Bet[];
  images: Image[];
};

export type Auction = {
  id: UUID;
  name: string;
  description: string;
  dateStart: Date;
  dateEnd: Date;
  authorId: UUID;
  state: State;
  lots: Lot[];
};
