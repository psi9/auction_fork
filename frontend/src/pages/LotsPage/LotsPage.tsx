import { AxiosInstance } from "axios";
import { useEffect, useState } from "react";

import LotCard from "../../components/cards/lotCard/LotCard";

import { Lot } from "../../domain/Entities";

import "./LotsPage.css";

export default function LotsPage(props: { client: AxiosInstance }) {
  const [lots, setLots] = useState<Lot[]>([]);

  useEffect(() => {
    async function getLots() {
      const response = await props.client.get("/api/lot/get_list");
      setLots(response.data);
    }

    getLots();
  }, [props.client]);

  return (
    <div className="main_container">
      {!lots.length ? (
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
