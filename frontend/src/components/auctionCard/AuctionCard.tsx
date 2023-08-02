import Button from "../button/Button"

import "./AuctionCard.css";

export default function Card() {
  return (
    <div className="card_container">
      <div className="title">What is Lorem Ipsum?</div>
      <div className="state">Статус: Ожидание</div>
      <div className="description">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type</div>
      <div className="container_info">
        <div className="dateStart">Дата начала: 2021-12-21, 20:43</div>
        <div className="dateEnd">Дата окончания: 2021-12-28, 10:20</div>
        <div className="author">Автор: Держапольский Константин</div>
      </div>
      <Button width="100%" text="Go to bidding" />
    </div>
  );
}
