import React from "react";
import AuthService from "../services/auth.service";

import ProfileDetails from "../components/ProfileDetails";

const Profile = () => {
  const currentUser = AuthService.getCurrentUser();

  if (currentUser === null)
    window.location.href = '/login';

  return (
      <ProfileDetails currentUser={currentUser}/>
  );
};

export default Profile;