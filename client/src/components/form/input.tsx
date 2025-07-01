import { useState } from 'react'
import { FaEye, FaEyeSlash } from 'react-icons/fa'

type Props = { name: string; password?: true; error?: boolean }

function Input({ name, error, password }: Props) {
  const [type, setType] = useState<'text' | 'password'>(
    password ? 'password' : 'text',
  )
  return (
    <div className="flex flex-col gap-2">
      <label
        htmlFor={name}
        className="capitalize text-text-secondary font-medium"
      >
        {name}
      </label>
      <div className="relative">
        <input
          type={type}
          name={name}
          className={`border border-border rounded ${error ? 'outline-red-500 outline-1' : 'focus:outline-secondary'} focus:outline-1 focus:outline-offset-1 px-2 py-1 opacity-80 focus:opacity-100 transition-all`}
        />
        {password && (
          <button
            type="button"
            className="absolute right-2 top-2 text-border hover:text-text-secondary transition-all cursor-pointer"
            onClick={() =>
              setType((prev) => (prev === 'password' ? 'text' : 'password'))
            }
          >
            {type === 'password' ? <FaEye /> : <FaEyeSlash />}
          </button>
        )}
      </div>
    </div>
  )
}

export default Input
