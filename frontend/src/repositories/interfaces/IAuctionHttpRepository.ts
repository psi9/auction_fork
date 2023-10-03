import { Auction } from "../../domain/Entities";
import IBaseHttpRepository from "./IBaseHttpRepository";

export default interface IAuctionHttpRepository
  extends IBaseHttpRepository<Auction> {}
