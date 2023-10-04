import { useEffect, useState } from "react";

import LotCard from "../../components/cards/lotCard/LotCard";

import { Lot } from "../../domain/Entities";

import "./LotsPage.css";
import Button from "../../components/button/Button";

export default function LotsPage() {
  const [lots, setLots] = useState<Lot[]>([]);

  return (
    <div className="main_container">
      {!lots.length ? (
        <div className="main_empty">
          <div className="empty">
            <div>Лотов пока нет.</div>
            <div>Будьте первым и создайте свой!</div>
          </div>
          <Button width="100%" text="Создать аукцион" />
        </div>
      ) : (
        lots.map((auction) => <LotCard key={auction.id} />)
      )}
    </div>
  );
}
