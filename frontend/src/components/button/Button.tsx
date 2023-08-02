import React from "react";
import "./Button.css";

interface ButtonProps {
  width: string;
  text: string;
}

const Button: React.FC<ButtonProps> = ({ width, text }) => {
  return (
    <button className="custom_button" style={{ width }}>
      {text}
    </button>
  );
};

export default Button;
