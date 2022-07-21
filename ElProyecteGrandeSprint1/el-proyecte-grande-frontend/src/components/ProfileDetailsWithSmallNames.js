import React from 'react'
import PropTypes from "prop-types";

const ProfileDetails = ({currentUser, closeProfile}) => {
    console.log(currentUser)
    return (
        <div className="container">
            {closeProfile &&
            <div>
                <button className="top-news-button btn-outline-light" type="button" onClick={closeProfile}>
                    <span className="top-news-button-content">Bak to Articles</span>
                </button>
            </div>}
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
    )
}


ProfileDetails.propTypes ={
  closeProfile: PropTypes.func
};

export default ProfileDetails