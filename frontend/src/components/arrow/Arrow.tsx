import React, { useEffect, useState } from "react";
import "./Arrow.css";

export default function Arrow() {
  const [showArrow, setShowArrow] = useState(false);

  useEffect(() => {
    function handleScroll() {
      const scrollY = window.scrollY;
      setShowArrow(scrollY >= 400);
    }

    window.addEventListener("scroll", handleScroll);
    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
  }, []);

  return (
    <div
      className={`btn_up ${showArrow ? "btn_up_show" : ""}`}
      onClick={() => { window.scrollTo({ top: 0, behavior: "smooth" })}}
    ></div>
  );
}

