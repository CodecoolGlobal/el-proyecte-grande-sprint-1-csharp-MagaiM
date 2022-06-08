import React, { Component, useEffect } from 'react';
import PropTypes from 'prop-types'
import { useState } from "react";

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