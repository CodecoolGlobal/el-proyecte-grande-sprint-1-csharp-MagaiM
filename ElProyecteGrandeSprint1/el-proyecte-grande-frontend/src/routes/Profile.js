import React from "react";
import AuthService from "../services/auth.service";

const Profile = () => {
  const currentUser = AuthService.getCurrentUser();

  if (currentUser === null)
    window.location.href = '/login';

  return (
    <div className="profile-container">
      <header className="jumbotron profile-name">
        <h3>
          <strong>{currentUser.UserName}</strong>&apos;s Profile
        </h3>
      </header>
      <div className="content-div">
        <div>
          <p className="profile-text">
            <strong>Token:</strong> {currentUser.AccessToken.substring(0, 20)} ...{" "}
            {currentUser.AccessToken.substr(currentUser.AccessToken.length - 20)}
          </p>
          <p className="profile-text">
            <strong>Email:</strong> {currentUser.Email}
          </p>

          <p className="profile-text">
            <strong>reputation:</strong> {currentUser.Reputation}
          </p>
          <strong className="profile-text">Authorities: </strong>
          <ul className="profile-text">
            {currentUser.Roles &&
              currentUser.Roles.map((role, index) => <li key={index}>{role}</li>)}
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Profile;