import Button from "../../components/button/Button";
import UserCard from "../../components/cards/userCard/UserCard";
import { useAuctionContext } from "../../contexts/AuctionContext";
import { useUserContext } from "../../contexts/UserContext";

export default function Auctions() {
  const users = useUserContext();
  const auctions = useAuctionContext();

  return (
    <div className="main_container">
      {!users.length ? (
        <div className="main_empty">
          <div className="empty">
            <div>Ни один пользователь пока не участвует</div>
            <div>Будьте первым!</div>
          </div>
          <Button width="100%" text="Я готов!" />
        </div>
      ) : (
        users.map((user) => (
          <UserCard
            key={user.id}
            user={user}
            auction={auctions.find((auction) => auction.authorId === user.id)}
          />
        ))
      )}
    </div>
  );
}
