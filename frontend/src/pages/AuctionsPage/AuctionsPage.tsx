import { useContext, useState } from "react";
import AuctionCard from "../../components/cards/auctionCard/AuctionCard";

import { Auction, User } from "../../objects/Entities";

import "./AuctionsPage.css";
import { UserAuthorizationContext } from "../../contexts/UserAuthorizationContext";
import { AuctionContext } from "../../contexts/AuctionContext";
import { enqueueSnackbar } from "notistack";

export default function AuctionsPage() {
  const { user, members } = useContext(UserAuthorizationContext);

  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");

  const { auctions, createAuction: createAuctionContext } =
    useContext(AuctionContext);

  const validateCreation = (): boolean => {
    if (!title || !description) {
      enqueueSnackbar("Заполните все поля", {
        variant: "error",
      });
      return false;
    }

    return true;
  };

  const resetState = () => {
    setTitle("");
    setDescription("");
  };

  const createAuction = async () => {
    if (!validateCreation()) return;

    await createAuctionContext(title, description, user?.id!);

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
          maxLength={30}
          onChange={(event) => setTitle(event.target.value)}
          placeholder="Введите название аукциона (до 30 символов)"
        />
        <textarea
          className="create_description"
          rows={10}
          value={description}
          maxLength={300}
          onChange={(event) => setDescription(event.target.value)}
          placeholder="Введите описание аукциона (до 300 символов)"
        ></textarea>
        <button className="submit_create" onClick={createAuction}>
          Создать
        </button>
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
                  members?.find(
                    (member: User) => member.id === auction.authorId
                  )!
                }
              />
            ))
            .reverse()
        )}
      </div>
    </div>
  );
}
