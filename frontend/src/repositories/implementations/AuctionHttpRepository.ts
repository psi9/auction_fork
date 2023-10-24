import { sendErrorNotice, sendWarnNotice } from "../../components/notification/Notification";
import { Auction, Result } from "../../objects/Entities";
import IAuctionHttpRepository from "../interfaces/IAuctionHttpRepository";

export default class AuctionHttpRepository implements IAuctionHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  async getAsync(): Promise<Result<Auction>> {
    try {
      const response = await fetch(`${this.baseURL}api/auction/get_list`);
      
      if (response.status === 401) {
        return {data: [], flag: false}
      }

      const data = await response.json();

      return {data, flag: true};
    } catch (error) {
      sendErrorNotice("Не удалось получить аукционы, попробуйте снова");
      return {data: [], flag: false}
    }
  }

  async postAsync(entity: Auction): Promise<boolean> {
    try {
      const response = await fetch(`${this.baseURL}api/auction/create`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify(entity),
      });

      if (response.status === 401) {
        sendWarnNotice("Вам необхожимо зарегистрироваться")
        return false;
      }

      return true;
    } catch (error) {
      sendErrorNotice("Не удалось создать аукцион, попробуйте снова");
      return false;
    }
  }

  async putAsync(entity: Auction): Promise<boolean> {
    try {
      const response = await fetch(`${this.baseURL}api/auction/update`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify(entity),
      });

      if (response.status === 401) {
        sendWarnNotice("Вам необходимо зарегистрироваться")
        return false;
      }

      return true;
    } catch (error) {
      sendErrorNotice("Не удалось изменить аукцион, попробуйте снова");
      return false;
    }
  }

  async deleteAsync(id: number): Promise<boolean> {
    try {
      const response = await fetch(`${this.baseURL}api/auction/delete/${id}`, {
        method: "DELETE",
      });

      if (response.status === 401) {
        sendWarnNotice("Вам необхожимо зарегистрироваться")
        return false;
      }

      return true;
    } catch (error) {
      sendErrorNotice("Не удалось удалить аукцион, попробуйте снова");
      return false;
    }
  }
}
