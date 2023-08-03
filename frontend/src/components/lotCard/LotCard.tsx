import Button from "../button/Button";

import "./LotCard.css";

export default function LotCard() {
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
          <div className="info_price">Начальная цена:</div>
          <div className="price_text">300р.</div>
        </div>
        <div>
          <div className="info_price">Цена выкупа:</div>
          <div className="price_text">1000р.</div>
        </div>
        <div className="bet_step">Шаг ставки:</div>
        <div className="step">50р.</div>
      </div>
      <Button width="100%" text="Сделать ставку" />
    </div>
  );
}
