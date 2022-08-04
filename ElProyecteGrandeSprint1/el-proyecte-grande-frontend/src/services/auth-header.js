export default function authHeader() {
  const user = JSON.parse(localStorage.getItem('user'));
  if (user && user.AccessToken) {
    return { Authorization: 'Bearer ' + user.AccessToken };
  } else {
    return {};
  }
}