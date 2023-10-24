import { useNavigate } from "react-router-dom";
import { Auction, User } from "../../../objects/Entities";
import { State } from "../../../objects/Enums";

import "./AuctionCard.css";
import { LotContext } from "../../../contexts/LotContext";
import { useContext } from "react";

export default function AuctionCard(props: { auction: Auction; author: User }) {
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

  const navigate = useNavigate();
  const { setAuctionId } = useContext(LotContext);

  function invite() {
    setAuctionId(props.auction.id);
    navigate("/auctions/lots");
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
      <button className="invite_button" onClick={invite}>
        Принять участие
      </button>
    </div>
  );
}
