import React from 'react'
import PropTypes from 'prop-types'
import { Link } from "react-router-dom";

const SearchByGameNewsButton = ({ button, index }) => {

  return (
    <>
      <button className="search-by-game-news-button btn-outline-light" type="button" key={index}>
        <Link className="nav-link text-light" type='button' to={`other-news?search=${button.title}`}>
          <h5>{button.title}</h5>
        </Link>
      </button><br />
    </>
  )
}

SearchByGameNewsButton.prototype = {
  showGameNews: PropTypes.func
}

export default SearchByGameNewsButton