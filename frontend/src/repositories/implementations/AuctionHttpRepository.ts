import { enqueueSnackbar } from "notistack";
import { Auction } from "../../objects/Entities";
import IAuctionHttpRepository from "../interfaces/IAuctionHttpRepository";
import { handleCommonRequest, handleCommonResponse } from "./RequestHandler";

export default class AuctionHttpRepository implements IAuctionHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  async getAsync(): Promise<Auction[] | undefined> {
    try {
      const response = await fetch(`${this.baseURL}api/auction/get-list`, {
        credentials: "include",
      });

      if (!handleCommonResponse(response)) return;

      return await response.json();
    } catch (error) {
      enqueueSnackbar("Не удалось получить аукционы, попробуйте снова", {
        variant: "error",
      });
    }
  }

  async getByIdAsync(id: string): Promise<Auction | undefined> {
    try {
      const response = await fetch(
        `${this.baseURL}api/auction/get-by-id/${id}`,
        {
          credentials: "include",
        }
      );

      if (!handleCommonResponse(response)) return;

      return await response.json();
    } catch (error) {
      enqueueSnackbar("Не удалось получить аукцион, попробуйте снова", {
        variant: "error",
      });
    }
  }

  async postAsync(entity: Auction): Promise<void> {
    try {
      await handleCommonRequest<void>(
        `${this.baseURL}api/auction/create`,
        "POST",
        entity
      );

      enqueueSnackbar("Аукцион создан успешно", { variant: "success" });
    } catch (error) {
      enqueueSnackbar("Не удалось создать аукцион, попробуйте снова", {
        variant: "error",
      });
    }
  }

  async putAsync(entity: Auction): Promise<void> {
    try {
      await handleCommonRequest<void>(
        `${this.baseURL}api/auction/update`,
        "PUT",
        entity
      );

      enqueueSnackbar("Аукцион успешно изменен", { variant: "success" });
    } catch (error) {
      enqueueSnackbar("Не удалось изменить аукцион, попробуйте снова", {
        variant: "error",
      });
    }
  }

  async deleteAsync(id: string): Promise<void> {
    try {
      await handleCommonRequest<void>(
        `${this.baseURL}api/auction/delete/${id}`,
        "DELETE",
        {}
      );

      enqueueSnackbar("Аукцион успешно удален", { variant: "success" });
    } catch (error) {
      enqueueSnackbar("Не удалось удалить аукцион, попробуйте снова", {
        variant: "error",
      });
    }
  }
}
