import React from 'react';
import PropTypes from 'prop-types'

const Container = ({text}) => {
  return (
    <div className='Container'>
        {text}
    </div>
  )
}

Container.prototype = {
    text: PropTypes.string
}

export default Container