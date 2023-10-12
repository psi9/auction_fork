import { useState } from "react";
import AuctionCard from "../../components/cards/auctionCard/AuctionCard";

import { useAuctionContext } from "../../contexts/AuctionContext";
import { useUserAuthorityContext } from "../../contexts/UserAuthorityContext";
import { useUserContext } from "../../contexts/UserContext";
import { Auction, User } from "../../objects/Entities";

import "./AuctionsPage.css";

export default function AuctionsPage() {
  const userAuthorityContext = useUserAuthorityContext();

  const [error, setError] = useState<string>("");

  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");

  const auctionContext = useAuctionContext();
  const auctions = auctionContext?.auctions;

  const users = useUserContext();

  const validateCreation = (): boolean => {
    if (!title.length || !description.length) {
      setError("Заполните все поля");
      return false;
    }

    if (title.length > 30 || description.length > 300) {
      setError("Вы превысили количество символов");
      return false;
    }

    return true;
  };

  const resetState = () => {
    setTitle("");
    setDescription("");
    setError("");
  };

  const createAuction = async () => {
    const curUser = userAuthorityContext?.user;

    if (!validateCreation()) return;

    await auctionContext?.createAuction(title, description, curUser?.id!);

    resetState();
  };

  return (
    <div className="main_box">
      <div className="input_box">
        <div className="title_create">Создайте свой аукцион</div>
        <input
          className="create_name"
          type="text"
          value={title}
          onChange={(event) => setTitle(event.target.value)}
          placeholder="Введите название аукциона (до 30 символов)"
        />
        <textarea
          className="create_description"
          rows={10}
          value={description}
          onChange={(event) => setDescription(event.target.value)}
          placeholder="Введите описание аукциона (до 300 символов)"
        ></textarea>
        <button className="submit_create" onClick={createAuction}>
          Создать
        </button>
        <div className="error">{error}</div>
      </div>
      <div className="main_container">
        {!auctions?.length ? (
          <div className="main_empty">
            <div className="empty">
              <div>Аукционов пока нет.</div>
              <div>Будьте первым и создайте свой!</div>
            </div>
          </div>
        ) : (
          auctions
            .map((auction: Auction) => (
              <AuctionCard
                key={auction.id}
                auction={auction}
                author={
                  users.find((user: User) => user.id === auction.authorId)!
                }
              />
            ))
            .reverse()
        )}
      </div>
    </div>
  );
}
