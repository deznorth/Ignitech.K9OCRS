import React from 'react'

/**
 * The old image was a raster image so it looked blurry. Using an SVG ensures that it'll always look crisp.
 */
export default function pawIcon(props) {
   return (
      <svg
         className={props?.className}
         viewBox="0 0 419.14468 403.60483"
         height={props?.height || '4rem'}
         width={props?.width || '4rem'}
      >
      <g
         id="layer1"
         transform="translate(-118.99908,-256.27405)"
      >
         <path fill="currentColor" stroke="none"
            d="M 281.78125 0 C 280.89861 0.011256107 279.99321 0.05451875 279.09375 0.125 C 243.27015 6.3085 223.57295 44.18935 220.71875 77.59375 C 216.54805 107.90515 229.9058 146.86425 263.1875 153.65625 C 268.015 154.57535 273.0311 154.1553 277.75 152.875 C 317.8269 139.4366 335.76175 91.97195 329.96875 52.65625 C 328.28477 27.263859 309.14316 -0.3489393 281.78125 0 z M 129.90625 1.625 C 107.63131 2.1717807 90.279662 24.7634 86.75 46 C 79.3091 88.0743 98.44834 140.35185 142.28125 153.65625 C 146.38695 154.54915 150.63375 154.6411 154.78125 154 C 184.40765 149.0625 196.95825 115.84615 195.71875 89.03125 C 194.82705 53.65875 176.45285 12.7581 139.71875 2.8125 C 136.36096 1.9220875 133.08838 1.5468885 129.90625 1.625 z M 378.84375 121.125 C 340.30945 123.4304 313.8911 161.87875 310.125 197.78125 C 305.0254 223.67495 318.8299 258.3078 348.375 260.375 C 389.5717 259.8672 417.67605 215.84645 418.84375 177.96875 C 421.44955 152.91745 406.69275 122.5082 378.84375 121.125 z M 41.5625 129.65625 C 25.168497 129.5152 9.0449188 139.33506 3.6875 156 C -10.6052 200.579 18.0964 257.03815 65.3125 266.40625 C 85.0181 269.77835 102.33285 254.6454 107.21875 236.4375 C 117.57335 197.4927 96.3054 152.27225 60.3125 134.59375 C 54.448578 131.30284 47.977544 129.71144 41.5625 129.65625 z M 214.3125 209.59375 C 182.17313 209.66229 149.52974 225.97193 128.71875 250.25 C 106.24135 278.5472 87.8285 311.4765 80.625 347.1875 C 71.8736 372.894 91.70839 402.47795 119.1875 402.65625 C 152.2464 403.57455 180.65825 380.8733 213.53125 379.1875 C 241.42135 374.9358 266.39435 389.4427 291.46875 398.9375 C 312.81805 408.0688 342.31905 404.56945 353.21875 381.59375 C 361.79235 358.17505 349.17285 333.20375 338.71875 312.40625 C 317.39655 278.64925 294.55365 243.17045 259.59375 222.09375 C 245.58084 213.40805 230.00847 209.56028 214.3125 209.59375 z "
            transform="translate(118.99908,256.27405)"
            id="path1575" />
      </g>
      </svg>
   )
}