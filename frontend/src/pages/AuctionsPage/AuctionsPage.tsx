import { useState, useEffect } from "react";

import Button from "../../components/button/Button";
import AuctionCard from "../../components/cards/auctionCard/AuctionCard";

import * as Entities from "../../domain/Entities";

import "./AuctionsPage.css";

export default function AuctionsPage() {
  const [auctions, setAuctions] = useState<Entities.Auction[]>([]);
  const [users, setUsers] = useState<Entities.User[]>([]);

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
        auctions.map((auction) => (
          <AuctionCard
            key={auction.id}
            auction={auction}
            author={users.find((user) => user.id === auction.authorId)!}
          />
        ))
      )}
    </div>
  );
}
