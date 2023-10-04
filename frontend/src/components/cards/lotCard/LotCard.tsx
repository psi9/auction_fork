import Button from "../../button/Button";

import { Lot } from "../../../domain/Entities";

import "./LotCard.css";

export default function LotCard() {
  return (
    <div className="card_container">
      {/* <div className="title">{props.lot.name}</div>
      <div className="state">{props.lot.state}</div>
      <div className="description">{props.lot.description}</div>
      <div className="container_info">
        <div>
          <div className="info_price">Начальная цена:</div>
          <div className="price_text">{props.lot.startPrice}p.</div>
        </div>
        <div>
          <div className="info_price">Цена выкупа:</div>
          <div className="price_text">{props.lot.buyoutPrice}p.</div>
        </div>
        <div className="bet_step">Шаг ставки:</div>
        <div className="step">{props.lot.betStep}p.</div>
      </div>
      <Button width="100%" text="Сделать ставку" /> */}
    </div>
  );
}
