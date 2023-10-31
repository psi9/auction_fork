import { useNavigate } from "react-router-dom";
import { Auction, User } from "../../../objects/Entities";
import { getStateFromEnum } from "../../../objects/Enums";

import "./AuctionCard.css";
import { LotContext } from "../../../contexts/LotContext";
import { useContext } from "react";

export default function AuctionCard(props: { auction: Auction; author: User }) {
  const navigate = useNavigate();
  const { setAuctionId } = useContext(LotContext);

  const dateStart = new Date(props.auction.dateStart);
  const dateEnd = new Date(props.auction.dateEnd);

  const invite = () => {
    setAuctionId(props.auction.id);
    navigate("/lots");
  };

  return (
    <div className="card_container">
      <div className="auction_title">{props.auction.name}</div>
      <div className="state">
        Статус: {getStateFromEnum(props.auction.state)}
      </div>
      <div className="description">{props.auction.description}</div>
      <div className="container_info">
        <div>
          <div className="info_date">Дата начала:</div>
          <div className="date_text">{dateStart.toLocaleString()}</div>
        </div>
        <div>
          <div className="info_date">Дата конца:</div>
          <div className="date_text">{dateEnd.toLocaleString()}</div>
        </div>
        <div className="author">{props.author.name}</div>
      </div>
      <button className="submit_create" onClick={invite}>
        Подробнее
      </button>
    </div>
  );
}
