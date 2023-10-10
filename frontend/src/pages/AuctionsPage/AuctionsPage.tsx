import { useNavigate } from "react-router-dom";

import AuctionCard from "../../components/cards/auctionCard/AuctionCard";

import { useAuctionContext } from "../../contexts/AuctionContext";
import { useUserAuthorityContext } from "../../contexts/UserAuthorityContext";
import { useUserContext } from "../../contexts/UserContext";
import { Auction, User } from "../../objects/Entities";

import "./AuctionsPage.css";
import { useEffect, useState } from "react";

export default function AuctionsPage() {
  const userAuthorityContext = useUserAuthorityContext();
  const navigate = useNavigate();

  useEffect(() => {
    if (!userAuthorityContext?.checkAccess()) navigate("/authority");
  });

  const [error, setError] = useState<string>("");

  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");

  const auctionContext = useAuctionContext();
  const auctions = auctionContext?.auctions;

  const users = useUserContext();

  const validateCreation = (): boolean => {
    return true;
  };

  const createAuction = async () => {
    const curUser = userAuthorityContext?.user;

    if (!validateCreation()) {
      setError("Ошибка");
      return;
    }

    await auctionContext?.createAuction(title, description, curUser?.id!);

    setTitle("");
    setDescription("");
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
          auctions.map((auction: Auction) => (
            <AuctionCard
              key={auction.id}
              auction={auction}
              author={users.find((user: User) => user.id === auction.authorId)!}
            />
          ))
        )}
      </div>
    </div>
  );
}
