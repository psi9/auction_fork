import { useState } from "react";

interface INotificationProps {
  message: string;
  color: string;
  onClose: () => void;
}

function Notification(props: {
  message: string;
  color: string;
  onClose: () => void;
}) {
  return (
    <div
      style={{
        position: "fixed",
        top: "1rem",
        right: "1rem",
        background: props.color,
        color: "white",
        padding: "1rem",
        borderRadius: "4px",
        boxShadow: "0 0 10px rgba(0, 0, 0, 0.2)",
      }}
    >
      {props.message}
      <button onClick={props.onClose}>Закрыть</button>
    </div>
  );
}

export default function NotificationManager() {
  const [notifications, setNotifications] = useState<Array<INotificationProps>>(
    []
  );

  const addNotification = (message: string, color: string) => {
    const newNotification = {
      message,
      color,
      onClose: () => removeNotification(newNotification),
    };
    setNotifications([...notifications, newNotification]);

    setTimeout(() => {
      removeNotification(newNotification);
    }, 5000);
  };

  const removeNotification = (notification: INotificationProps) => {
    setNotifications(notifications.filter((n) => n !== notification));
  };

  return (
    <div>
      {notifications.map((notification, index) => (
        <Notification key={index} {...notification} />
      ))}
    </div>
  );
}
