import Button from "../../button/Button";
import { Auction, User } from "../../../domain/Entities";
import { State } from "../../../domain/Enums";

import "./AuctionCard.css";

export default function Card(props: { auction: Auction; author: User }) {
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
      <div className="auction_title">{props.auction.name}</div>
      <div className="state">Статус: {getState(props.auction.state)}</div>
      <div className="description">{props.auction.description}</div>
      <div className="container_info">
        <div>
          <div className="info_date">Дата начала:</div>
          <div className="date_text">
            {new Date(props.auction.dateStart).toLocaleString()}
          </div>
        </div>
        <div>
          <div className="info_date">Дата конца:</div>
          <div className="date_text">
            {new Date(props.auction.dateEnd).toLocaleDateString()}
          </div>
        </div>
        <div className="author">{props.author.name}</div>
      </div>
      <Button width="100%" text="Принять участие" />
    </div>
  );
}
