using System.Text;
using System.Text.RegularExpressions;
using teste_pucrs;

namespace teste_pucrs
{
    public class ScholarshipProgram
    {
        static public List<Scholarship> scholars;


        //Cria o menu visual e possibilita a escolha das opções 
        static public void CreateMenu()
        {
            Console.Clear();
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine("|                                     ");
            Console.WriteLine("|        Boa tarde, bem vindo ao      ");
            Console.WriteLine("|         programa de bolsistas       ");
            Console.WriteLine("|                                     ");
            Console.WriteLine("|           Escolha sua opção         ");
            Console.WriteLine("|                                     ");
            Console.WriteLine("|  1- [Consultar bolsa zero/Ano]      ");
            Console.WriteLine("|  2- [Codificar nomes]               ");
            Console.WriteLine("|  3- [Consultar média anual]         ");
            Console.WriteLine("|  4- [Ranking valores de bolsa]      ");
            Console.WriteLine("|  5- [Terminar o programa]           ");
            Console.WriteLine("|                                     ");
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine();
            //Verificação do input, forçando a escolha de um número
            int choosedOption;
            var tempInput = Console.ReadLine();
            bool validInput = int.TryParse(tempInput, out choosedOption);
            //Validação se a escolha é realmente um número.
            while (!validInput)
            {
                Console.WriteLine("Você não digitou um número, tente novamente!");
                tempInput = Console.ReadLine();
                validInput = int.TryParse(tempInput, out choosedOption);
            }
            //Direciona as opções baseado na entrada
            switch (choosedOption)
            {
                case 1:
                    ShowScholarshipZero();
                    break;
                case 2:
                    ShowCodedName();
                    break;
                case 3:
                    ShowYearAverage();
                    break;
                case 4:
                    ShowMinMaxScholars();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Você não digitou uma opção válida, pressione enter!");
                    Console.ReadLine();
                    break;
            }
        }

        //Método que cria o gráfico e lida com a validação do input baseado em ano.
        static public int ShowYearMenu()
        {
            Console.Clear();
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine("|                                     ");
            Console.WriteLine("|      Digite o ano escolhido:        ");
            Console.WriteLine("|                                     ");
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine();

            //Mesma validação de input do CreateMenu
            int choosedYear;
            var tempInput = Console.ReadLine();
            bool validInput = int.TryParse(tempInput, out choosedYear);
            while (!validInput)
            {
                Console.WriteLine("Você não digitou um número válido, tente novamente!");
                Console.WriteLine();
                tempInput = Console.ReadLine();
                validInput = int.TryParse(tempInput, out choosedYear);
            }
            return choosedYear;
        }

        //Método que cria o menu visual da busca de bolsa zero por ano.
        static public void ShowScholarshipZero()
        {
            int choosedYear = ShowYearMenu();
            //Uso do método de busca de todos as bolsas do menor mes do ano, porem guardando apenas a 1 bolsa em ordem alfabética.
            List<Scholarship> tempStudents = SearchFirstMonthByYear(choosedYear, ref scholars);
            while (tempStudents.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Não existem bolsistas nesse ano, pressione enter! ");
                Console.ReadLine();
                choosedYear = ShowYearMenu();
                tempStudents = SearchFirstMonthByYear(choosedYear, ref scholars);
            }
            Scholarship zero = tempStudents[0];
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine("|                                                 ");
            Console.WriteLine("| O bolsista zero do ano escolhido é  ");
            Console.WriteLine("|                                     ");
            Console.WriteLine("|  NOME: " + zero.name);
            Console.WriteLine("|  CPF: " + zero.cpf);
            Console.WriteLine("|  IES: " + zero.ies);
            Console.WriteLine("|  VALOR DA BOLSA: R$ " + zero.scholarshipValue);
            Console.WriteLine("|                                                 ");
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine();
            Console.ReadLine();
        }

        //Método que cria o menu visual da busca e codificação do nome
        static public void ShowCodedName()
        {
            Console.Clear();
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine("|                                      ");
            Console.WriteLine("|       Digite o nome do bolsista:     ");
            Console.WriteLine("|                                      ");
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine();

            //Registra a entrada e valida que seja formada apenas de letras
            string tempInput = Console.ReadLine();
            bool validString = Helpers.OnlyAlphabet(tempInput);
            List<Scholarship> students;
            //Mantém o usuário no loop até que se tenha uma entrada válida
            while (!validString)
            {
                Console.WriteLine("Você não digitou um nome válido, tente novamente!");
                Console.WriteLine();
                tempInput = Console.ReadLine();
                validString = Helpers.OnlyAlphabet(tempInput);
            }
            students = SearchByName(tempInput, ref scholars);
            //Limita a busca para 10 participantes 
            //Não houve especificação de quantos registros, porem uma busca muito genérica retornaria muitos
            //Levando a codificação de nomes indesejados
            // if (students.Count > 10 || students.Count == 0)
            // {
            //Garante que o usuário especifique até que haja uma busca com menos de 10 Bolsistas
            while (students.Count > 10 || students.Count == 0)
            {
                if (students.Count > 10)
                {
                    Console.WriteLine("Muitos resultados na pesquisa, por favor seja mais especifico!");
                    Console.WriteLine();
                    tempInput = Console.ReadLine();
                    validString = Helpers.OnlyAlphabet(tempInput);
                    students = SearchByName(tempInput, ref scholars);
                }
                //Garante que o usuário digite um nome existente na base de Bolsistas
                if (students.Count == 0)
                {
                    if (students.Count == 0 && validString)
                        Console.WriteLine("Aluno não existente na base de dados, por favor verifique o nome!");
                    else
                        Console.WriteLine("Você não digitou um nome válido, tente novamente!");
                    Console.WriteLine();
                    tempInput = Console.ReadLine();
                    validString = Helpers.OnlyAlphabet(tempInput);
                    students = SearchByName(tempInput, ref scholars);
                    //  }
                }
            }

            // if (students.Count == 0)
            // {
            //     while (students.Count == 0)
            //     {
            //         if (students.Count == 0 && validString)
            //             Console.WriteLine("Aluno não existente na base de dados, por favor verifique o nome!");
            //         else
            //             Console.WriteLine("Você não digitou um nome válido, tente novamente!");
            //         Console.WriteLine();
            //         tempInput = Console.ReadLine();
            //         validString = Helpers.OnlyAlphabet(tempInput);
            //         students = SearchByName(tempInput, ref scholars);
            //     }
            // }

            //Embaralha os nomes e depois encripta usando Cifra de César com 1 Shift para a direita
            var encryptedStudents = EncryptScholars(students);

            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine("|                                                 ");
            Console.WriteLine("|          Os " + encryptedStudents.Count + " bolsistas mais próximos          ");
            Console.WriteLine("|             da sua pesquisa são:                ");
            Console.WriteLine("|                                                 ");
            Console.WriteLine("|-------------------------------------------------");

            foreach (var crypted in encryptedStudents)
            {
                Console.WriteLine("|                                                 ");
                Console.WriteLine("|   NOME: " + crypted.name);
                Console.WriteLine("|   ANO: " + crypted.year);
                Console.WriteLine("|   IES: " + crypted.ies);
                Console.WriteLine("|   VALOR DA BOLSA: " + crypted.scholarshipValue);
                Console.WriteLine("|                                                 ");
                Console.WriteLine("|-------------------------------------------------");
            }
            Console.ReadLine();
        }

        //Método que cria o menu visual da contagem de média das bolsas do ano escolhido.
        static public void ShowYearAverage()
        {
            //Mostra o menu de escolha do ano e chama o método para calcular a média do mesmo.
            int choosedYear = ShowYearMenu();
            int yearAverage = GetYearAverage(choosedYear, ref scholars);
            while (yearAverage == -1)
            {
                Console.WriteLine();
                Console.WriteLine("Não existem bolsistas nesse ano, pressione enter! ");
                Console.ReadLine();
                choosedYear = ShowYearMenu();
                yearAverage = GetYearAverage(choosedYear, ref scholars);
            }
            Console.Clear();
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine("|                                                 ");
            Console.WriteLine("|    A média das bolsas do ano: " + choosedYear);
            Console.WriteLine("|               R$ " + yearAverage);
            Console.WriteLine("|                                                 ");
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine();
            Console.ReadLine();

        }

        //Método que cria o menu visual da busca das 3 menores e maiores bolsas 
        static public void ShowMinMaxScholars()
        {
            Scholarship[] minMax = Get3MinMax(scholars);
            Console.Clear();
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine("|                                                 ");
            Console.WriteLine("|    As três menores bolsas foram: ");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("|                                                 ");
                Console.WriteLine("|    NOME: " + minMax[i].name);
                Console.WriteLine("|    CPF: " + minMax[i].cpf);
                Console.WriteLine("|    IES: " + minMax[i].ies);
                Console.WriteLine("|    VALOR: " + minMax[i].scholarshipValue);
                Console.WriteLine("|    ANO: " + minMax[i].year);
                Console.WriteLine("|                                                 ");
                Console.WriteLine("|-------------------------------------------------");
            }
            Console.WriteLine();
            Console.WriteLine("|-------------------------------------------------");
            Console.WriteLine("|                                                 ");
            Console.WriteLine("|    As três maiores bolsas foram: ");
            for (int i = 5; i >= 3; i--)
            {
                Console.WriteLine("|                                                 ");
                Console.WriteLine("|    NOME: " + minMax[i].name);
                Console.WriteLine("|    CPF: " + minMax[i].cpf);
                Console.WriteLine("|    IES: " + minMax[i].ies);
                Console.WriteLine("|    VALOR: " + minMax[i].scholarshipValue);
                Console.WriteLine("|    ANO: " + minMax[i].year);
                Console.WriteLine("|                                                 ");
                Console.WriteLine("|-------------------------------------------------");
            }
            Console.ReadLine();

        }

        //Método que faz uma busca em todos os alunos do ano, contabiliza o valor de suas bolsas e retorna a média anual.
        static public int GetYearAverage(int year, ref List<Scholarship> scholarships)
        {
            int yearTotal = 0;
            int yearStudents = 0;
            //Para todo estudante com a bolsa no ano escolhido, adiciona seu valor no total, e adiciona 1 a contagem de estudantes.
            foreach (Scholarship scholar in scholarships)
                if (scholar.year == year)
                {
                    yearTotal += scholar.scholarshipValue;
                    yearStudents++;
                }
            if (yearStudents == 0)
                return -1;
            return yearTotal / yearStudents;
        }

        //Método que faz a busca em todos os alunos, contabiliza as 3 menores e maiores bolsas e retorna elas em uma lista.
        static public Scholarship[] Get3MinMax(List<Scholarship> scholarships)
        {
            Scholarship[] minMaxArray = new Scholarship[6];
            //Organiza a lista em ordem crescente baseado no ano
            scholarships.Sort((scholar1, scholar2) => scholar1.year.CompareTo(scholar2.year));
            //Descresce a lista
            scholarships.Reverse();
            //Organiza a lista em ordem crescente baseado no valor da bolsa
            scholarships.Sort((scholar1, scholar2) => scholar1.scholarshipValue.CompareTo(scholar2.scholarshipValue));

            //Coloca os 3 primeiros Bolsistas no array de retorno
            minMaxArray[0] = scholarships[0];
            minMaxArray[1] = scholarships[1];
            minMaxArray[2] = scholarships[2];
            //Coloca os 3 ultimos Bolsistas no array de retorno
            minMaxArray[3] = scholarships[scholarships.Count - 3];
            minMaxArray[4] = scholarships[scholarships.Count - 2];
            minMaxArray[5] = scholarships[scholarships.Count - 1];
            return minMaxArray;
        }

        //Método que usa do StreamReader para ler o CSV, criando um objeto Bolsista e colocando na lista para retorno.
        static public List<Scholarship> LoadCSVData(string relativePath)
        {
            //Criação do StreamReader usando um caminho relativo a pasta do projeto
            var reader = new StreamReader(File.OpenRead(relativePath));
            List<Scholarship> tempScholars = new List<Scholarship>();
            //Pulo da primeira linha do csv, contendo o nome das colunas
            reader.ReadLine();
            //Criação dos Bolsistas baseado no CSV entrege, com 11 colunas, sendo utilizada as 5 primeiras e ultima
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                if (values.Length != 11)
                {
                    Console.WriteLine("CSV incompativel! Faltam dados.");
                    return null;
                }
                //FIXO - Melhoria a ser feita, suporta somente um tipo de CSV com ordem e tamanho especifico.
                Scholarship tempScholar = new Scholarship();
                tempScholar.name = Helpers.RemoveAccents(values[0].ToUpper());
                tempScholar.cpf = values[1];
                tempScholar.ies = values[2];
                if (!int.TryParse(values[3], out tempScholar.month))
                    Console.WriteLine("Erro carregando o mês de inicio de bolsa! ");
                if (!int.TryParse(values[4], out tempScholar.year))
                    Console.WriteLine("Erro carregando o ano de inicio de bolsa! ");
                if (!int.TryParse(values[values.Length - 1], out tempScholar.scholarshipValue))
                    Console.WriteLine("Erro carregando valor da bolsa! ");
                tempScholars.Add(tempScholar);
            }
            tempScholars.Sort((x, y) => string.Compare(x.name, y.name));
            scholars = tempScholars;
            return tempScholars;
        }

        //Método que retorna todos os Bolsistas de menor mês no ano escolhido.
        static public List<Scholarship> SearchFirstMonthByYear(int year, ref List<Scholarship> scholarships)
        {
            //Algoritmo baseado em algoritmos de sort, mantem os alunos com menor mes guardados na lista
            //Caso haja um aluno com mes de valor menor, refaz a lista, mantendo apenas esse
            //Continua a iteração procurando novos alunos do mesmo mes ou menor.
            List<Scholarship> students = new List<Scholarship>();
            foreach (Scholarship scholar in scholarships)
            {
                if (scholar.year == year)
                {
                    //Adiciona o primeiro aluno para comparação
                    if (students.Count == 0)
                    {
                        students.Add(scholar);
                    }
                    //Cria a nova lista caso encontre aluno com mes menor
                    else if (scholar.month < students[0].month)
                    {
                        students = new List<Scholarship>();
                        students.Add(scholar);
                    }
                    //Adiciona alunos do mesmo mes do aluno 0 da lista
                    else if (scholar.month == students[0].month)
                    {
                        students.Add(scholar);
                    }
                }
            }
            //Organiza a lista em ordem alfabetica
            students.Sort((x, y) => string.Compare(x.name, y.name));
            //Salva a lista em um txt, para testes
            //Helpers.SaveScholarshipToTXT("out.txt", ref students);
            //Retorna os bolsistas.
            return students;
        }

        //Método que retorna todos os Bolsistas que tem a string de busca no seu nome, independente do local.
        static public List<Scholarship> SearchByName(string searchName, ref List<Scholarship> scholarships)
        {
            List<Scholarship> students = new List<Scholarship>();
            //Testa para cada estudante se existe a string em seu nome.
            foreach (Scholarship student in scholarships)
            {
                if (student.name.Contains(searchName.ToUpper()))
                    students.Add(student);
            }
            return students;
        }

        //Método que troca as posições das letras do inicio-fim até o meio, e aplica a cifra de césar com 1-right shift
        static public List<Scholarship> EncryptScholars(List<Scholarship> scholarships)
        {
            List<Scholarship> tempScholarships = new List<Scholarship>();
            foreach (Scholarship scholar in scholarships)
            {
                //Embaralha o nome e já chama a Cifra
                var name = Helpers.CaesarCypher(Helpers.ScrambleLetters(scholar.name), 1);
                scholar.name = name;
                tempScholarships.Add(scholar);
            }
            return tempScholarships;
        }
    }

    class Helpers
    {
        //Regex feito com ajuda do StackOverflow, que busca o Non Spacing Mark(Caracteres feitos para serem unidos a outro, sem usar mais espaço)
        private readonly static Regex nonSpacingMarkRegex =
           new Regex(@"\p{Mn}", RegexOptions.Compiled);

        public static string RemoveAccents(string text)
        {
            if (text == null)
                return string.Empty;
            //Decompoe o texto separando acentos de letras. ex: É -> E'
            var normalizedText =
                text.Normalize(NormalizationForm.FormD);
            //Transforma tudo que seja Non Spacing Mark em texto vazio.
            return nonSpacingMarkRegex.Replace(normalizedText, string.Empty);
        }

        //Feito para vias de teste, salva qualquer lista de Bolsistas com seu nome, cpf, ies e data de entrada
        public static void SaveScholarshipToTXT(string txtName, ref List<Scholarship> students)
        {
            //Cria uma stream no modo de Escrita
            FileStream filestream = new FileStream("resources/" + txtName, FileMode.Create);
            //Cria uma "gravadora" de stream baseado na stream acima
            var streamwriter = new StreamWriter(filestream);
            //Automaticamente limpa a stream
            streamwriter.AutoFlush = true;
            //Escolhe a gravadora como saida do Console.Write
            TextWriter standard = Console.Out;
            Console.SetOut(streamwriter);
            foreach (Scholarship scholar in students)
            {
                Console.WriteLine(scholar.name + " " + scholar.cpf);
                Console.WriteLine(scholar.ies);
                Console.WriteLine(scholar.month + " " + scholar.year); ;
            }
            //Volta o Console.Write ao STDOUT
            Console.SetOut(standard);
        }

        public static bool OnlyAlphabet(string testString)
        {
            foreach (char c in testString)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    return false;
            }
            return true;
        }

        public static string ScrambleLetters(string toScramble)
        {
            StringBuilder sb = new StringBuilder(toScramble);
            //Começa invertendo o primeiro e ultimo caracter, e continua com segundo/penultimo...
            for (int i = 0; i < toScramble.Length / 2; i++)
            {
                char TempCharEndIt = toScramble[toScramble.Length - i - 1];
                char TempCharStartIt = toScramble[i];
                sb[i] = TempCharEndIt;
                sb[toScramble.Length - i - 1] = TempCharStartIt;
            }
            //Caso a palavra tenha mais de 3 letras, inverte novamente o primeiro e ultimo caracter
            if (sb.Length > 3)
            {
                char TempCharEnd = toScramble[toScramble.Length - 1];
                char TempCharStart = toScramble[0];
                sb[toScramble.Length - 1] = TempCharEnd;
                sb[0] = TempCharStart;
            }
            return sb.ToString();
        }

        //Implementação adaptada da Cifra de César, coerente com a necessidade. 
        public static string CaesarCypher(string toEncrypt, int key)
        {
            StringBuilder sb = new StringBuilder(String.Empty);
            //Anda pelos caracteres da string, testa se é uma letra maiuscula ou não para decidir o offset
            foreach (char ch in toEncrypt)
            {
                //Caso não seja uma letra, porem espaço, apenas adiciona no nome e segue
                if (!char.IsLetter(ch) && char.IsWhiteSpace(ch))
                {
                    sb.Append(ch);
                    continue;
                }
                //Caso não seja uma letra, retorna null, pois um outro tipo de caracter seria transformado
                //Em um caracter não relacionado 
                else if (!char.IsLetter(ch))
                    return null;

                //Como todos os Bolsistas são tratados 100% com nomes em maiúsculo é desnecessário, 
                //mas é uma redundancia de segurança, decide o offset baseado no caracter ser Ou não maiúsculo.
                char offset = char.IsUpper(ch) ? 'A' : 'a';

                //Pega o valor na tabela do caracter, adiciona a chave e remove o offset, modula em 26 para
                //Quando chegar em um caracter que somado a chave recomece o alfabeto, seja feito esse loop
                //E readiciona o offset para chegar ao caracter final
                sb.Append((char)((((ch + key) - offset) % 26) + offset));
            }
            return sb.ToString();
        }
    }
}
public class MainProgram
{

    static public void Main(String[] args)
    {
        ScholarshipProgram.LoadCSVData(@"resources\br-capes-bolsistas-uab.csv");
        while (true)
        {
            ScholarshipProgram.CreateMenu();
        }
    }


}

