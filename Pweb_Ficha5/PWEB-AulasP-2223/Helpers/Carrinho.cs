namespace PWEB_AulasP_2223.Helpers
{
    public class Carrinho
    {
        public List<CarrinhoItem> items { get; set; } = new List<CarrinhoItem>();
        public void AddItem(CarrinhoItem curso, int qtd) {
            CarrinhoItem item = items.Where(b => b.CursoId == curso.CursoId).FirstOrDefault();
            if (item == null)
            {
                items.Add(new CarrinhoItem { CursoId = curso.CursoId, Quantidade = qtd, PrecoUnit = curso.PrecoUnit, CursoNome = curso.CursoNome });
                
            }
            else
            {
                item.Quantidade += qtd;
            }
        }
        public void RemoveItem(CarrinhoItem curso) { 
            items.RemoveAll(l => l.CursoId == curso.CursoId);
        }
        public decimal Total() =>  items.Sum(i => i.PrecoUnit * i.Quantidade);
        
        public void Clear() => items.Clear();

    }
}
