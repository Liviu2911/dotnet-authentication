type Props = {
  children: React.ReactNode
}

function Form({ children }: Props) {
  return (
    <div className="absolute left-0 top-0 w-full h-[100vh] flex items-center justify-center">
      <form className="flex flex-col items-center gap-4">{children}</form>
    </div>
  )
}

export default Form
