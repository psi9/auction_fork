import Button from "../../button/Button";

import { Lot } from "../../../objects/Entities";

import "./LotCard.css";
import { State } from "../../../objects/Enums";

export default function LotCard(props: { lot: Lot }) {
  const buyoutLot =
    props.lot.buyoutPrice === 0 ? "Не выкуплен" : `${props.lot.buyoutPrice}р.`;

  function getState(state: State): string {
    switch (state) {
      case State.awaiting: {
        return "Ожидание";
      }
      case State.editing: {
        return "Редактирование";
      }
      case State.running: {
        return "Запушен";
      }
      case State.completed: {
        return "Завершен";
      }
      case State.canceled: {
        return "Отменен";
      }
    }
  }

  return (
    <div className="card_container">
      <div className="title">{props.lot.name}</div>
      <div className="state">Статус: {getState(props.lot.state)}</div>
      <div className="description">{props.lot.description}</div>
      <div className="container_info">
        <div>
          <div className="info_price">Начальная цена:</div>
          <div className="price_text">{props.lot.startPrice}p.</div>
        </div>
        <div>
          <div className="info_price">Цена выкупа:</div>
          <div className="price_text">{buyoutLot}</div>
        </div>
        <div className="bet_step">Шаг ставки:</div>
        <div className="step">{props.lot.betStep}p.</div>
      </div>
      <Button width="100%" text="Сделать ставку" />
    </div>
  );
}
