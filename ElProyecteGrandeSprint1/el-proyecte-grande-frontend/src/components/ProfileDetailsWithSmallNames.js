import React from 'react'
import PropTypes from "prop-types";

const ProfileDetailsWithSmallNames = ({ currentUser, closeProfile }) => {
    return (
        <div className="container">
            <div>
                <button className="top-news-button btn-outline-light" type="button" onClick={closeProfile}>
                    <span className="top-news-button-content">Back</span>
                </button>
            </div>
            <header className="jumbotron">
                <h3>
                    <strong>{currentUser.userName}&apos;s</strong> Profile
                </h3>
            </header>
            <p>
                <strong>ID:</strong> {currentUser.id}
            </p>
            <p>
                <strong>Email:</strong> {currentUser.email}
            </p>
            <p>
                <strong>Reputation:</strong> {currentUser.reputation}
            </p>
            <strong>Authorities:</strong>
            <ul>
                {currentUser.roles &&
                    currentUser.roles.map((role, index) => <li key={index}>{role["name"]}</li>)}
            </ul>
        </div>
    )
}
ProfileDetailsWithSmallNames.propTypes = {
    closeArticle: PropTypes.func
};
export default ProfileDetailsWithSmallNames