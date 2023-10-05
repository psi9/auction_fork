import LotCard from "../../components/cards/lotCard/LotCard";

import "./LotsPage.css";
import Button from "../../components/button/Button";

import { useLotContext } from "../../contexts/LotContext";

export default function LotsPage() {
  const lots = useLotContext();

  return (
    <div className="main_container">
      {!lots.length ? (
        <div className="main_empty">
          <div className="empty">
            <div>Лотов пока нет.</div>
            <div>Будьте первым и создайте свой!</div>
          </div>
          <Button width="100%" text="Создать лот" />
        </div>
      ) : (
        lots.map((lot) => <LotCard key={lot.id} lot={lot} />)
      )}
    </div>
  );
}
