import { Auction } from "../../objects/Entities";
import IBaseHttpRepository from "./IBaseHttpRepository";

export default interface IAuctionHttpRepository
  extends IBaseHttpRepository<Auction> {
  getByIdAsync(id: string): Promise<Auction | undefined>;
}
