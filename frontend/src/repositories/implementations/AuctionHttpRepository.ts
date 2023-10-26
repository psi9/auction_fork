import { enqueueSnackbar } from "notistack";
import { Auction, Result } from "../../objects/Entities";
import IAuctionHttpRepository from "../interfaces/IAuctionHttpRepository";

export default class AuctionHttpRepository implements IAuctionHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  async getAsync(): Promise<Result<Auction>> {
    try {
      const response = await fetch(`${this.baseURL}api/auction/get_list`, {
        credentials: "include",
      });

      if (response.status === 401) {
        return { data: [], flag: false };
      }

      const data = await response.json();

      return { data, flag: true };
    } catch (error) {
      enqueueSnackbar("Не удалось получить аукционы, попробуйте снова", {
        variant: "error",
      });
      return { data: [], flag: false };
    }
  }

  async getByIdAsync(id: string): Promise<Auction | undefined> {
    try {
      const response = await fetch(
        `${this.baseURL}api/auction/get_by_id/${id}`,
        {
          credentials: "include",
        }
      );

      if (response.status === 401) {
        return;
      }

      const data = await response.json();

      return data;
    } catch (error) {
      enqueueSnackbar("Не удалось получить аукцион, попробуйте снова", {
        variant: "error",
      });
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
        credentials: "include",
      });

      if (response.status === 401) {
        enqueueSnackbar("Вам необхожимо зарегистрироваться", {
          variant: "warning",
        });
        return false;
      }

      enqueueSnackbar("Аукцион создан успешно", { variant: "success" });

      return true;
    } catch (error) {
      enqueueSnackbar("Не удалось создать аукцион, попробуйте снова", {
        variant: "error",
      });
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
        credentials: "include",
      });

      if (response.status === 401) {
        enqueueSnackbar("Вам необходимо зарегистрироваться", {
          variant: "warning",
        });
        return false;
      }

      enqueueSnackbar("Аукцион успешно изменен", { variant: "success" });

      return true;
    } catch (error) {
      enqueueSnackbar("Не удалось изменить аукцион, попробуйте снова", {
        variant: "error",
      });
      return false;
    }
  }

  async deleteAsync(id: string): Promise<boolean> {
    try {
      const response = await fetch(`${this.baseURL}api/auction/delete/${id}`, {
        method: "DELETE",
        credentials: "include",
      });

      if (response.status === 401) {
        enqueueSnackbar("Вам необхожимо зарегистрироваться", {
          variant: "warning",
        });
        return false;
      }

      enqueueSnackbar("Аукцион успешно удален", { variant: "success" });

      return true;
    } catch (error) {
      enqueueSnackbar("Не удалось удалить аукцион, попробуйте снова", {
        variant: "error",
      });
      return false;
    }
  }
}
