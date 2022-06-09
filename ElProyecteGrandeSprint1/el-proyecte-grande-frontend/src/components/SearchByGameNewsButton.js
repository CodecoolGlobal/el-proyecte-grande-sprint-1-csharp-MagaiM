import React from 'react'
import PropTypes from 'prop-types'

const SearchByGameNewsButton = ({button, showGameNews}) => {
  
  return (
    <>
        <button className="search-by-game-news-button btn-outline-light" type="button" onClick={showGameNews}><h5>{button.title}</h5></button><br/>
    </>
  )
}

SearchByGameNewsButton.prototype = {
    showGameNews: PropTypes.func
}

export default SearchByGameNewsButton