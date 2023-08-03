import Button from "../button/Button";

import "./AuctionCard.css";

export default function Card() {
  return (
    <div className="card_container">
      <div className="title">What is Lorem Ipsum?</div>
      <div className="state">Статус: Ожидание</div>
      <div className="description">
        Lorem Ipsum is simply dummy text of the printing and typesetting
        industry. Lorem Ipsum has been the industry's standard dummy text ever
        since the 1500s, when an unknown printer took a galley of type
      </div>
      <div className="container_info">
        <div>
          <div className="info_date">Дата начала:</div>
          <div className="date_text">2021-12-21, 20:43</div>
        </div>
        <div>
          <div className="info_date">Дата конца:</div>
          <div className="date_text">2021-12-28, 10:20</div>
        </div>
        <div className="author">Держапольский Константин</div>
      </div>
      <Button width="100%" text="Принять участие" />
    </div>
  );
}
