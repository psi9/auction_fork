import Button from "../button/Button"

import "./Card.css";

export default function Card() {
  return (
    <div className="card_container">
      <div className="title">What is Lorem Ipsum?</div>
      <div className="description">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type</div>
      <Button width="100%" text="Go to bidding" />
    </div>
  );
}
