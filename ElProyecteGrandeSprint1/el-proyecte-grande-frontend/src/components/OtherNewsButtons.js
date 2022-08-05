import React from 'react'
import SearchByGameNewsButton from './SearchByGameNewsButton'

const OtherNewsButtons = () => {
  const buttons = [
    { title: "Overwatch" },
    { title: "World of Warcraft" },
    { title: "Warframe" },
    { title: "Fortnite" },
    { title: "League of Legends" },
    { title: "Minecraft" },
    { title: "Borderlands" },
    { title: "Skyrim" },
    { title: "Resident Evil" },
    { title: "Hollow Knight" },
    { title: "Mega Man" },
    { title: "Sonic the Hedgehog" },
    { title: "Pokemon" },
    { title: "Call of Duty" },
    { title: "Grand Theft Auto" },
    { title: "The Sims" },
    { title: "Tetris" },
    { title: "Assassin's Creed" },
    { title: "Final Fantasy" }
  ]

  return (
    <div className="other-news">
      <h2>Popular Games:</h2>
      <br />
      {buttons.map((button, index) => (
        <SearchByGameNewsButton key={index} button={button} />
      ))}
    </div>
  )
}

export default OtherNewsButtons