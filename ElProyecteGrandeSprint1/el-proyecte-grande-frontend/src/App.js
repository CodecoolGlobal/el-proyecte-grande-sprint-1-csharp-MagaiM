import React, { useState, useEffect } from "react";
import { Routes, Route } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "./Design/App.css";
import AuthService from "./services/auth.service";
import authHeader from "./services/auth-header";
import Login from "./routes/Login";
import Register from "./routes/Register";
import Home from "./routes/Home";
import Profile from "./routes/Profile";
import BoardUser from "./components/BoardUser";
import BoardModerator from "./components/BoardModerator";
import BoardAdmin from "./routes/BoardAdmin";
import ForgotPassword from "./routes/ForgotPassword";
import Header from './components/Header';
import News from './routes/News';
import Deals from './routes/Deals';
import RecentNews from './routes/RecentNews';
import OtherNews from './routes/OtherNews';
import Articles from "./routes/Articles";
import ArticleEditor from "./routes/ArticleEditor";
import NewPassword from "./routes/NewPassword";


const App = () => {
  const [showModeratorBoard, setShowModeratorBoard] = useState(false);
  const [showAdminBoard, setShowAdminBoard] = useState(false);
  const [currentUser, setCurrentUser] = useState(undefined);



  useEffect(() => {
    const localUser = AuthService.getCurrentUser();
    if (localUser && !currentUser) setCurrentUser(localUser)
    if (currentUser) {
      setShowModeratorBoard(currentUser.Roles.includes("Moderator"));
      setShowAdminBoard(currentUser.Roles.includes("Admin"));
    }
  }, [currentUser]);

  return (
    <div className='bg-dark text-white app-container'>
      <Header currentUser={currentUser} showModeratorBoard={showModeratorBoard} showAdminBoard={showAdminBoard} />
      <div className="container mt-3">
        <Routes>
          <Route path="/" element={<Home fetchData={fetchData} />} />
          <Route path="/home" element={<Home fetchData={fetchData} />} />
          <Route path="/news" element={<News />}>
            <Route path='' element={<RecentNews fetchData={fetchData} />} />
            <Route path='/news/other-news' element={<OtherNews fetchData={fetchData} />} />
          </Route>
          <Route path="deals" element={<Deals fetchData={fetchData} />} />
          <Route path="/login" element={<Login setCurrentUser={setCurrentUser} />} />
          <Route path="/register" element={<Register />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/user" element={<BoardUser />} />
          <Route path="/mod" element={<BoardModerator />} />
          <Route path="/admin" element={<BoardAdmin />} />
          <Route path="/forgotPassword" element={<ForgotPassword />} />
          <Route path="/articles" element={<Articles />} />
          <Route path="/articles-editor/:Id" element={<ArticleEditor />} />
          <Route path="*" element={
            <main style={{ padding: "1rem" }}>
              <p>There&apos;s nothing here!</p>
            </main>} />
          <Route path={`/newPassword/:emailId`} element={<NewPassword />} />
        </Routes>
      </div>
    </div>
  );
};

async function fetchData(url) {
  const response = await fetch(url, { headers: authHeader() });
  if (response.ok) {
    const data = await response.json();
    return data;
  }
  throw response;
}

export default App;