import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Header from "./components/header/Header";
import Footer from "./components/footer/Footer";
import Arrow from "./components/arrow/Arrow";

import AuctionsPage from "./pages/AuctionsPage/AuctionsPage";
import AuthorityPage from "./pages/AuthorityPage/AuthorityPage";
import LotsPage from "./pages/LotsPage/LotsPage";

import "./App.css";
import { AuctionProvider } from "./contexts/AuctionContext";
import { UserProvider } from "./contexts/UserContext";
import { LotProvider } from "./contexts/LotContext";
import Breadcrumbs from "./components/breadcrumbs/Breadcrumbs";
import { UserAuthorityProvider } from "./contexts/UserAuthorityContext";

import NotFoundPage from "./pages/NotFoundPage/NotFoundPage";
import ProfilePage from "./pages/ProfilePage/ProfilePage";

function App() {
  return (
    <div className="App">
      <Router>
        <UserAuthorityProvider>
          <Header />
          <Breadcrumbs separator=">" />
          <AuctionProvider>
            <LotProvider>
              <UserProvider>
                <Routes>
                  <Route path="/auctions" element={<AuctionsPage />}></Route>
                  <Route path="/authority" element={<AuthorityPage />}></Route>
                  <Route path="/auctions/lots" element={<LotsPage />}></Route>
                  <Route path="/profile" element={<ProfilePage />}></Route>
                  <Route path="*" element={<NotFoundPage />} />
                </Routes>
              </UserProvider>
            </LotProvider>
          </AuctionProvider>
        </UserAuthorityProvider>
      </Router>
      <Arrow />
      <Footer />
    </div>
  );
}

export default App;
