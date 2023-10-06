import { useNavigate } from "react-router-dom";

import Button from "../../components/button/Button";
import AuctionCard from "../../components/cards/auctionCard/AuctionCard";

import { useAuctionContext } from "../../contexts/AuctionContext";
import { useUserAuthorityContext } from "../../contexts/UserAuthorityContext";
import { useUserContext } from "../../contexts/UserContext";
import { Auction, User } from "../../objects/Entities";

import "./AuctionsPage.css";
import { useEffect } from "react";

export default function AuctionsPage() {
  const userAuthorityContext = useUserAuthorityContext();
  const navigate = useNavigate();

  useEffect(() => {
    if (!userAuthorityContext?.checkAccess()) navigate("/authority");
  });

  const auctions = useAuctionContext();
  const users = useUserContext();

  return (
    <div className="main_container">
      {!auctions.length ? (
        <div className="main_empty">
          <div className="empty">
            <div>Аукционов пока нет.</div>
            <div>Будьте первым и создайте свой!</div>
          </div>
          <Button width="100%" text="Создать аукцион" />
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
  );
}
