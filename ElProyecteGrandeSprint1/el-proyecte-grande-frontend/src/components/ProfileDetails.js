import React from 'react'

const ProfileDetails = ({currentUser}) => {
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
  )
}

export default ProfileDetails