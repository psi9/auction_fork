import { sendErrorNotice, sendWarnNotice } from "../../components/notification/Notification";
import { Lot, Result } from "../../objects/Entities";
import ILotHttpRepository from "../interfaces/ILotHttpRepository";

export default class LotHttpRepository implements ILotHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  async getAsync(): Promise<Result<Lot>> {
    try {
      const response = await fetch(`${this.baseURL}api/lot/get_list`);
     
      if (response.status === 401) {
        return {data: [], flag: false};
      }

      const data = await response.json();

      return {data, flag: true}
    } catch (error) {
      sendErrorNotice("Не удалось получить лоты, попробуйте снова");
      return {data: [], flag: false};
    }
  }

  async getByAuctionAsync(auctionId: string): Promise<Result<Lot>> {
    try {
      const response = await fetch(
        `${this.baseURL}api/lot/get_list_by_auction/${auctionId}`
      );
      
      if (response.status === 401) {
        sendWarnNotice("Вам необходимо зарегистрироваться")
        return {data: [], flag: false};
      }

      const data = await response.json();

      return {data, flag: true};
    } catch (error) {
      sendErrorNotice("Не удалось получить лоты по аукциону, попробуйте снова");
      return {data: [], flag: false};
    }
  }

  async postAsync(entity: Lot): Promise<boolean> {
    try {
      const response = await fetch(`${this.baseURL}api/lot/create`, {
        method: "POST",
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
      sendErrorNotice("Не удалось создать лот, попробуйте снова");
      return false;
    }
  }

  async putAsync(entity: Lot): Promise<boolean> {
    try {
      const response = await fetch(`${this.baseURL}api/lot/update`, {
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
      sendErrorNotice("Не удалось изменить лот, попробуйте снова");
      return true;
    }
  }

  async deleteAsync(id: number): Promise<boolean> {
    try {
      const response = await fetch(`${this.baseURL}api/lot/delete/${id}`, {
        method: "DELETE",
      });

      if (response.status === 401) {
        sendWarnNotice("Вам необходимо зарегистрироваться")
        return false;
      }

      return true;
    } catch (error) {
      sendErrorNotice("Не удалось удалить лот, попробуйте снова");
      return false;
    }
  }
}
