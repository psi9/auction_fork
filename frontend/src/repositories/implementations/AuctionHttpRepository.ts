import { Auction } from "../../domain/Entities";
import IAuctionHttpRepository from "../interfaces/IAuctionHttpRepository";

export default class AuctionHttpRepository implements IAuctionHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  getAsync(): Auction[] {
    throw new Error("Method not implemented.");
  }

  postAsync(entity: Auction): void {
    throw new Error("Method not implemented.");
  }

  putAsync(entity: Auction): void {
    throw new Error("Method not implemented.");
  }

  deleteAsync(id: number): void {
    throw new Error("Method not implemented.");
  }
}
