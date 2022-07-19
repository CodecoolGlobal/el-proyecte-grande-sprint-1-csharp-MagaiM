import React, { useEffect } from "react";
import AuthService from "../services/auth.service";
import { useNavigate } from 'react-router-dom';
import { BrowserRouter as Router, Switch, 
  Route, Redirect,} from "react-router-dom";

const Profile = () => {
  const currentUser = AuthService.getCurrentUser();
  const navigate = useNavigate();

  if (currentUser === null) 
    window.location.replace("/");

  return (
    <div className="container">
      <header className="jumbotron">
        <h3>
          <strong>{currentUser.UserName}</strong> Profile
        </h3>
      </header>
      <p>
        <strong>Token:</strong> {currentUser.AccessToken.substring(0, 20)} ...{" "}
        {currentUser.AccessToken.substr(currentUser.AccessToken.length - 20)}
      </p>
      <p>
        <strong>Email:</strong> {currentUser.Email}
      </p>
      <strong>Authorities:</strong>
      <ul>
        {currentUser.Roles &&
          currentUser.Roles.map((role, index) => <li key={index}>{role}</li>)}
      </ul>
    </div>
  );
};

export default Profile;